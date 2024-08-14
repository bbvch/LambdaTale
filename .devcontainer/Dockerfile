FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:c02c4bf93bae986f3eac2bb4599bd8b11eb14ae61d31e81d00f9ecb7f802c7b0
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
