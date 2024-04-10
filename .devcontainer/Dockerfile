FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:7c3b3091f0fd089ecbffbb3131a1660caa59097a6921d6f2caad17f1abc3d56f
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
