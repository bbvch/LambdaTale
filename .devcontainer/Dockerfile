FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:d7d85a913836fd10ab270b08790a2ad960fe7232c064e81a8b7842b5699c8374
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
