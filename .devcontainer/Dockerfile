FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:bdced679b0d7031b802a4e2ead0b3f339473f30e61a52c2f675069efe2e8f7b2
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
