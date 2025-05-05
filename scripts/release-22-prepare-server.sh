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
[[ -z "${REGISTRY}" ]] && REGISTRY="ghcr.io"
[[ -z "${CONFIGURATION}" ]] && CONFIGURATION="Release"

dotnet restore --locked-mode

dotnet publish ./src/Arkanis.Overlay.Host.Server/Arkanis.Overlay.Host.Server.csproj \
    --no-restore \
    --configuration ${CONFIGURATION} \
    -p:PublishProfile=DefaultContainer \
    -p:ContainerRegistry=${REGISTRY} \
    -p:ContainerImageTag="${VERSION_TAG}"
