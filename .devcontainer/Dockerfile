FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:f759d174817a58bd059dab036fc9a3d069c7c31f1c2b77a917f0f0a8e9b7d534
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
