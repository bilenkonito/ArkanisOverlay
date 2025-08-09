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
[[ -z "${GITHUB_REPOSITORY+x}" ]] && GITHUB_REPOSITORY="ArkanisCorporation/ArkanisOverlay"
[[ -z "${REGISTRY+x}" ]] && REGISTRY="ghcr.io"

IMAGE_NAME=$(echo "${REGISTRY}/${GITHUB_REPOSITORY}" | tr '[:upper:]' '[:lower:]')

>&2 echo "Building the server web application container..."
docker build -f ./src/Arkanis.Overlay.Host.Server/Dockerfile -t "${IMAGE_NAME}:${VERSION_TAG}" -t "${IMAGE_NAME}":latest .

>&2 echo "Pushing the server web application container..."
docker push "${IMAGE_NAME}:${VERSION_TAG}"
docker push "${IMAGE_NAME}":latest

>&2 echo "Successfully published the server web application container"
