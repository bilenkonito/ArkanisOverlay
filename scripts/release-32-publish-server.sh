#!/usr/bin/env bash
set -eEuo pipefail

### publishCmd
#
#| Command property | Description                                                                                                                                                                                                                                        |
#| ---------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
#| `exit code`      | Any non `0` code is considered as an unexpected error and will stop the `semantic-release` execution with an error.                                                                                                                                |
#| `stdout`         | The `release` information can be written to `stdout` as parseable JSON (for example `{"name": "Release name", "url": "http://url/release/1.0.0"}`). If the command write non parseable JSON to `stdout` no `release` information will be returned. |
#| `stderr`         | Can be used for logging.

[[ -z "${VERSION_TAG+x}" ]] && echo "VERSION_TAG is not set" && exit 2
[[ -z "${REGISTRY+x}" ]] && REGISTRY="ghcr.io"
[[ -z "${CONFIGURATION+x}" ]] && CONFIGURATION="Release"

>&2 echo "Publishing the server web application container..."
dotnet publish ./src/Arkanis.Overlay.Host.Server/Arkanis.Overlay.Host.Server.csproj \
    --configuration "${CONFIGURATION}" \
    -p:PublishProfile=DefaultContainer \
    -p:ContainerRegistry="${REGISTRY}" \
    -p:ContainerImageTag="${VERSION_TAG}" \
    -p:DebugType=None \
    -p:DebugSymbols=false \
    1>&2

>&2 echo "Successfully published the server web application container"
