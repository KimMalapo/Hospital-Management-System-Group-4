using Xunit;

// CurrentUser is a static value and the integration tests share one database,
// so tests must not run in parallel.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
