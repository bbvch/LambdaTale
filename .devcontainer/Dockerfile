FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:b878b60a68aadab1a1d7e7ace12504073ce5a6ce568c785f54a9ef4b834e373f
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
