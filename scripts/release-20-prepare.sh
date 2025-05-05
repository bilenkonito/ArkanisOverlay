#!/usr/bin/env bash

### prepareCmd
#
#| Command property | Description                                                                                                         |
#| ---------------- | ------------------------------------------------------------------------------------------------------------------- |
#| `exit code`      | Any non `0` code is considered as an unexpected error and will stop the `semantic-release` execution with an error. |
#| `stdout`         | Can be used for logging.                                                                                            |
#| `stderr`         | Can be used for logging.                                                                                            |

[[ -z "${VERSION}" ]] && >&2 echo "VERSION is not set" && exit 2
[[ -z "${VERSION_TAG}" ]] && >&2 echo "VERSION_TAG is not set" && exit 2
[[ -z "${CONFIGURATION}" ]] && CONFIGURATION="Release"

"$(dirname "$(realpath "$0")")/release-21-prepare-win.sh"
