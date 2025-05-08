#!/usr/bin/env bash
set -eEuo pipefail

### prepareCmd
#
#| Command property | Description                                                                                                         |
#| ---------------- | ------------------------------------------------------------------------------------------------------------------- |
#| `exit code`      | Any non `0` code is considered as an unexpected error and will stop the `semantic-release` execution with an error. |
#| `stdout`         | Can be used for logging.                                                                                            |
#| `stderr`         | Can be used for logging.                                                                                            |

[[ -z "${VERSION+x}" ]] && >&2 echo "VERSION is not set" && exit 2
[[ -z "${VERSION_CHANNEL+x}" ]] && >&2 echo "VERSION_CHANNEL is not set" && exit 2
[[ -z "${GITHUB_TOKEN+x}" ]] && >&2 echo "GITHUB_TOKEN is not set" && exit 2
[[ -z "${REPOSITORY_URL+x}" ]] && REPOSITORY_URL="https://github.com/ArkanisCorporation/ArkanisOverlay"

[[ -d publish ]] || >&2 echo "publish directory does not exist" && exit 2

>&2 echo "Downloading previous release to build a delta release..."
dotnet vpk download github \
    --repoUrl "${REPOSITORY_URL}" \
    --token "${GITHUB_TOKEN}"

>&2 echo "Packing the published application..."
dotnet vpk [win] pack \
    --packTitle "Arkanis Overlay" \
    --packId Arkanis.Overlay.Application \
    --splashImage ./src/Arkanis.Overlay.Application/Resources/ArkanisTransparent_512x512.png \
    --icon ./src/Arkanis.Overlay.Application/Resources/favicon.ico \
    --packVersion "${VERSION}" \
    --framework net8.0-x64-desktop \
    --channel "${VERSION_CHANNEL}" \
    --packDir publish \
    --outputDir release
