FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:e63cd0d513e0d5023a01ca30f0df71cd2b9a9aeab77a3820b2a72ccd2f209037
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
