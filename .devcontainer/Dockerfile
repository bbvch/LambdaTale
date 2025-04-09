FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:3e1cda9410eed7ec3d8f422556bf70e564820c284b370862c571de791d2e7112
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
