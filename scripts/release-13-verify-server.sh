#!/usr/bin/env bash
set -eEuo pipefail

### verifyReleaseCmd
#
#| Command property | Description                                                              |
#| ---------------- | ------------------------------------------------------------------------ |
#| `exit code`      | `0` if the verification is successful, or any other exit code otherwise. |
#| `stdout`         | Only the reason for the verification to fail can be written to `stdout`. |
#| `stderr`         | Can be used for logging.                                                 |

[[ -z "${VERSION_TAG+x}" ]] && echo "VERSION_TAG is not set" && exit 2
[[ -z "${REGISTRY+x}" ]] && REGISTRY="ghcr.io"
[[ -z "${CONFIGURATION+x}" ]] && CONFIGURATION="Release"

>&2 echo "Building server web application container..."
dotnet publish ./src/Arkanis.Overlay.Host.Server/Arkanis.Overlay.Host.Server.csproj \
    --configuration "${CONFIGURATION}" \
    --output publish-server \
    -p:PublishProfile=DefaultContainer \
    -p:ContainerImageTag="${VERSION_TAG}" \
    -p:DebugType=None \
    -p:DebugSymbols=false \
    1>&2 # logging output must not go to stdout

>&2 echo "Successfully built the server web application container"
