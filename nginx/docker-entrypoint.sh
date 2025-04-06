#!/usr/bin/env sh
set -eu

envsubst '${DOMAIN},${STAGING_URL}' < "/etc/nginx/conf.d/default.conf.template" > /etc/nginx/nginx.conf

exec "$@"