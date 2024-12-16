FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:f84f5dc61134aacd099c03f58663994da48047a6f43b9d5ba446a5b5ad2074aa
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
