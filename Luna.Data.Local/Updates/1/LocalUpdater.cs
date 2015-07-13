using System.Collections.Generic;

namespace Luna.Data.Local.SQLite.Updates.NewMoon
{
    internal class LocalUpdater : SchemaUpdater
    {
        protected override Queue<string> GetUpgradeScripts()
        {
            var buffer = new Queue<string>();
            buffer.Enqueue(@"
CREATE TABLE `Repositories`
(
    Id  GUID PRIMARY KEY,
    name TEXT,
    is_online INTEGER,
    schema_version NUMERIC,
    parameters TEXT,
    last_update DATE,
    version GUID);
");
            buffer.Enqueue(@"
CREATE TABLE `ApplicationParameters`
(
    name  TEXT PRIMARY KEY,
    value TEXT);
");

            buffer.Enqueue(@"
CREATE TABLE `ExternalAccounts`
(
    Id  GUID PRIMARY KEY,
    name TEXT,
    provider_type TEXT,
    parameters TEXT,
    is_default_contacts INTEGER,
    is_default_mail INTEGER,
    geolocalizer INTEGER,
    contacts INTEGER
    mail INTEGER);
");
            buffer.Enqueue(@"
 CREATE TABLE `Addresses` (
    `Id`	GUID NOT NULL,
    `address`	TEXT,
    `zipCode`	TEXT NOT NULL,
    `city`	TEXT NOT NULL,
    `country`	TEXT,
    PRIMARY KEY(Id));
");
            buffer.Enqueue(@"
CREATE TABLE `Contacts` (
    `Id`	GUID NOT NULL,
    `Id_address`	GUID,
    `name`	TEXT NOT NULL,
    `surname`	TEXT,
    `title`	TEXT,
    `phone`	TEXT,
    `mobile`	TEXT,
    `phone_second`	TEXT,
    `mobile_second`	TEXT,
    `fax`	TEXT,
    `email`	TEXT,
    `email_second`	TEXT,
    `no_mail` INTEGER,
    `skype`	TEXT,
    `linkedin`	TEXT,
    `twitter`	TEXT,
    `facebook`	TEXT,
    `company`	TEXT,
    `description`	TEXT,
    `source`	TEXT,
    `comment`	TEXT,
    PRIMARY KEY(id)
);
");
            buffer.Enqueue(@"
CREATE TABLE `Tags` (
    `id_tag` GUID NOT NULL,
    `id_repository` GUID NOT NULL,
    `name` TEXT NOT NULL,
    `color` TEXT NULL,
    `last_update` DATE NOT NULL,
    `version` GUID NOT NULL,
    PRIMARY KEY(id_tag)
);
");
            buffer.Enqueue(@"
CREATE TABLE `R_Contacts_Tags` (
    `Id_contact`	GUID NOT NULL,
    `Id_tag`	GUID NOT NULL
);
");

            buffer.Enqueue("INSERT INTO SchemaVersions ( Version) VALUES(1);");

            return buffer;
        }

        protected override Queue<string> GetDowngradeScripts()
        {
            var buffer = new Queue<string>();
            buffer.Enqueue("DROP TABLE Providers;");
            buffer.Enqueue("DROP TABLE Repositories;");

            return buffer;
        }

        public override decimal Version
        {
            get { return 1; }
        }
    }
}