from abc import ABC, abstractmethod
import logging
from typing import Any
from sqlalchemy import Connection, text, Engine, CursorResult


logger = logging.getLogger(__name__)


class Migration(ABC):
    def __init__(self, conn: Connection) -> None:
        super().__init__()
        self.conn = conn
        pass

    @abstractmethod
    def upgrade(self):
        """Apply the migration to upgrade the database schema to a new version."""
        pass

    def execute(self, query: str, params: dict | None = None) -> CursorResult[Any]:
        return self.conn.execute(text(query), params)

    def create_table_if_not_exists(self, table_name: str, columns: list[str]):
        columns_str = ", ".join(columns)
        self.execute(
            f"CREATE TABLE IF NOT EXISTS {table_name} ({columns_str})")

    def get_current_version(self) -> int:
        self.create_table_if_not_exists(
            "schema_version", ["version INT PRIMARY KEY"])
        result = self.execute("SELECT version FROM schema_version LIMIT 1") \
            .fetchone()
        logger.debug(f"Fetched schema version: {result}")
        if result is None:
            self.execute("INSERT INTO schema_version (version) VALUES (-1)")
            return -1
        return result[0]

    def set_version(self, version: int):
        self.execute("UPDATE schema_version SET version = :v",
                     {"v": version})
