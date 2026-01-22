import logging
from migrations.migration import Migration, logger
from sqlalchemy import Connection, Engine
import importlib

logger = logging.getLogger(__name__)

class MigrationManager(Migration):
    def __init__(self, conn: Connection) -> None:
        super().__init__(conn)
        pass

    @staticmethod
    def apply_migrations(dataEngine: Engine) -> None:
        with dataEngine.begin() as conn:
            MigrationManager(conn).upgrade()

    def upgrade(self):
        migrations = {
            0: "init_schema.InitSchema",
        }

        current_version = self.get_current_version()
        logger.info(f"Current schema version: {current_version}")

        for version, module_name in sorted(migrations.items()):
            if version > current_version:
                migration_def = module_name.split('.')
                (module_name, class_name) = migration_def[0], migration_def[1]
                logger.info(f"Applying migration to v{version} ({class_name})...")
                module = importlib.import_module(f'migrations.{module_name}')
                migrate_class = getattr(module, class_name)
                migrate: Migration = migrate_class(self.conn)
                migrate.upgrade()
                self.set_version(version)
                logger.info(f"Migration to v{version} ({class_name}) done")

