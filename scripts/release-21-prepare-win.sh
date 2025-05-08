#!/usr/bin/env bash
set -eEuo pipefail

### prepareCmd
#
#| Command property | Description                                                                                                         |
#| ---------------- | ------------------------------------------------------------------------------------------------------------------- |
#| `exit code`      | Any non `0` code is considered as an unexpected error and will stop the `semantic-release` execution with an error. |
#| `stdout`         | Can be used for logging.                                                                                            |
#| `stderr`         | Can be used for logging.                                                                                            |

[[ -z "${CONFIGURATION+x}" ]] && CONFIGURATION="Release"

dotnet publish ./src/Arkanis.Overlay.Application/Arkanis.Overlay.Application.csproj \
    --runtime win-x64 \
    --configuration "${CONFIGURATION}" \
    --output publish \
    -p:EnableWindowsTargeting=true \
    -p:DebugType=None \
    -p:DebugSymbols=false

cp CHANGELOG.md publish/

cd publish || (>&2 echo "Failed switching directory to publish" && exit 1)
zip -r ../ArkanisOverlay.zip .
