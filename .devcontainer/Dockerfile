FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:f95660477c993355665e82543ef200747f5c8df1410da1a94ff7fa81cfe2478a
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
