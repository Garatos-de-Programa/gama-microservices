CREATE ROLE national_geographic_admin LOGIN PASSWORD 'change_me';

GRANT rds_superuser TO national_geographic_admin;

CREATE DATABASE NationalGeographicDb;
GRANT ALL PRIVILEGES ON DATABASE NationalGeographicDb TO national_geographic_admin;

CREATE EXTENSION postgis;
CREATE EXTENSION postgis_raster;
CREATE EXTENSION fuzzystrmatch;
CREATE EXTENSION postgis_tiger_geocoder;
CREATE EXTENSION postgis_topology;
CREATE EXTENSION address_standardizer_data_us;

SET search_path=public,tiger;

SELECT postGIS_extensions_upgrade();
