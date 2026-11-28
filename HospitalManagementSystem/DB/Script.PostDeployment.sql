-- Post-deployment script: runs after every publish, so every statement must be re-runnable.
-- Seeds example master data only when missing.

:r .\Seed\Seed_Lookups.sql
:r .\Seed\Seed_Services.sql
:r .\Seed\Seed_Demo.sql
