using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace HospitalManagementSystem.Tests.Integration;

/// <summary>
/// Creates a throwaway LocalDB database, runs every script from the DB project into it
/// (tables, triggers, stored procedures, post-deployment seed), and drops it afterwards.
/// If LocalDB is not available the tests are skipped; if a script fails the tests fail.
/// </summary>
public sealed class LocalDbFixture : IDisposable
{
    private const string Server = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;TrustServerCertificate=true;";
    private readonly string _dbName = "HMS_Test_" + Guid.NewGuid().ToString("N");
    private string _dbFolder = "";

    public bool LocalDbAvailable { get; private set; }
    public string? SkipReason { get; private set; }
    public string ConnectionString => $"{Server}Initial Catalog={_dbName};";
    public List<string> ScriptErrors { get; } = new();

    public LocalDbFixture()
    {
        try
        {
            Exec($"{Server}Initial Catalog=master;", $"CREATE DATABASE [{_dbName}]");
            LocalDbAvailable = true;
        }
        catch (Exception ex)
        {
            SkipReason = "SQL Server LocalDB is not available: " + ex.Message;
            return;
        }

        try
        {
            _dbFolder = FindDbFolder();
            foreach (var group in new[] { "Table", "Trigger", "StoredProcedure" })
                RunGroup(group);
            RunPostDeployment();
        }
        catch (Exception ex)
        {
            ScriptErrors.Add("Database test setup failed: " + ex.Message);
        }
    }

    public void RunPostDeployment()
    {
        var path = Path.Combine(_dbFolder, "Script.PostDeployment.sql");
        if (!File.Exists(path)) { ScriptErrors.Add("Script.PostDeployment.sql not found in " + _dbFolder); return; }
        try { RunFile(path); }
        catch (Exception ex) { ScriptErrors.Add($"Script.PostDeployment.sql: {ex.Message}"); }
    }

    private void RunGroup(string folderName)
    {
        var files = Directory.GetFiles(Path.Combine(_dbFolder, folderName), "*.sql", SearchOption.AllDirectories)
                             .OrderBy(f => f, StringComparer.OrdinalIgnoreCase).ToList();
        var errors = new Dictionary<string, string>();

        // Tables may depend on each other (foreign keys). Retry the failures until nothing more succeeds.
        for (var pass = 0; pass < 6 && files.Count > 0; pass++)
        {
            var failed = new List<string>();
            foreach (var f in files)
            {
                try { RunFile(f); errors.Remove(f); }
                catch (Exception ex) { failed.Add(f); errors[f] = ex.Message; }
            }
            if (failed.Count == files.Count) break;
            files = failed;
        }
        foreach (var kv in errors)
            ScriptErrors.Add($"{Path.GetFileName(kv.Key)}: {kv.Value}");
    }

    private void RunFile(string path)
    {
        foreach (var batch in SplitBatches(ReadWithIncludes(path)))
            Exec(ConnectionString, batch);
    }

    /// <summary>Inlines ":r file.sql" lines (SQLCMD include) so the post-deployment script can be run without SQLCMD.</summary>
    private static string ReadWithIncludes(string path)
    {
        var dir = Path.GetDirectoryName(path)!;
        var lines = File.ReadAllLines(path).Select(line =>
        {
            var m = Regex.Match(line, @"^\s*:r\s+(.+?)\s*$", RegexOptions.IgnoreCase);
            if (!m.Success) return line;
            var inc = Path.GetFullPath(Path.Combine(dir, m.Groups[1].Value.Trim('"')));
            return ReadWithIncludes(inc);
        });
        return string.Join(Environment.NewLine, lines);
    }

    private static IEnumerable<string> SplitBatches(string script) =>
        Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase)
             .Select(b => b.Trim())
             .Where(b => b.Length > 0);

    private static void Exec(string connectionString, string sql)
    {
        using var conn = new SqlConnection(connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.CommandTimeout = 60;
        cmd.ExecuteNonQuery();
    }

    /// <summary>Finds the DB project folder, or uses the HMS_DB_FOLDER environment variable.</summary>
    private static string FindDbFolder()
    {
        var fromEnv = Environment.GetEnvironmentVariable("HMS_DB_FOLDER");
        if (!string.IsNullOrWhiteSpace(fromEnv) && Directory.Exists(fromEnv)) return fromEnv;

        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            foreach (var candidate in new[]
                     {
                         Path.Combine(dir.FullName, "DB"),
                         Path.Combine(dir.FullName, "HospitalManagementSystem", "DB")
                     })
            {
                if (File.Exists(Path.Combine(candidate, "DB.sqlproj"))) return candidate;
            }
            dir = dir.Parent;
        }
        throw new DirectoryNotFoundException("Could not find the DB project folder. Set HMS_DB_FOLDER.");
    }

    public void Dispose()
    {
        if (!LocalDbAvailable) return;

        SqlConnection.ClearAllPools();
        Exec($"{Server}Initial Catalog=master;",
             $"IF DB_ID(N'{_dbName}') IS NOT NULL BEGIN ALTER DATABASE [{_dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{_dbName}]; END");

        using var conn = new SqlConnection($"{Server}Initial Catalog=master;");
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT DB_ID(@name)";
        cmd.Parameters.AddWithValue("@name", _dbName);
        if (cmd.ExecuteScalar() is not DBNull and not null)
            throw new InvalidOperationException($"Temporary database '{_dbName}' was not dropped.");
    }
}

[CollectionDefinition("LocalDb")]
public class LocalDbCollection : ICollectionFixture<LocalDbFixture> { }
