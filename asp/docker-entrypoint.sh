#!/usr/bin/env bash

set -e

envsRawest="$(printenv)"

readarray -t envsRaw < <(printf '%s\n' "$envsRawest")

readonly FILENAME=".env.cron"
[ -e "$FILENAME" ] && rm "$FILENAME"

for var in "${envsRaw[@]}"; do
  delimiter='='
  key=${var%%"$delimiter"*}
  val=${var#*"$delimiter"}
  echo "$key='$val'" >> "$FILENAME"
done

service cron start

/app/scheduler.sh
