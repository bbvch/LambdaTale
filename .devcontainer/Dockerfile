FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:fdb71370c88e3be3c51a8401143c454634fa55fd3c1e0be0c598d642480dd01f
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
