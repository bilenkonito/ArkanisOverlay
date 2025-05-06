#!/usr/bin/env bash
set -eEuo pipefail

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

dotnet publish ./src/Arkanis.Overlay.Application/Arkanis.Overlay.Application.csproj \
    --no-restore \
    --runtime win-x64 \
    --configuration ${CONFIGURATION} \
    --output publish

cd publish || >&2 echo "Failed switching directory to publish" && exit 1

cp ../CHANGELOG.md .
zip -r ../ArkanisOverlay.zip .
