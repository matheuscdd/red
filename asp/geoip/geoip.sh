#!/usr/bin/env bash

set -e

readonly TEMP_DIR='temp'
readonly TAR_GZ_FILE='GeoLite2-City.tar.gz'
readonly DB_DIR='database'
readonly FILENAME='GeoLite2-City.mmdb'

mkdir -p "$TEMP_DIR" "$DB_DIR"
curl -L -u "$GEO_IP_ACCOUNT_ID:$GEO_IP_LICENSE_KEY" \
'https://download.maxmind.com/geoip/databases/GeoLite2-City/download?suffix=tar.gz' \
-o "$TEMP_DIR/$TAR_GZ_FILE"
tar -xvzf "$TEMP_DIR/$TAR_GZ_FILE" -C "$TEMP_DIR"
readonly FILEPATH=$(find "$TEMP_DIR" -name "$FILENAME")
cp "$FILEPATH" "$DB_DIR/$FILENAME"
rm -r "$TEMP_DIR"
