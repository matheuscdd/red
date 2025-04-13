#!/usr/bin/env bash

set -e

readonly path='/app'
readonly logsDir="$path/logs"
cd "$path" || exit
[ ! -d "$logsDir" ] && mkdir "$logsDir"

source .env.cron
envs="$(tr '\n' ' ' < .env.cron)"
eval "$envs /geoip/geoip.sh >> $logsDir/cron.log"