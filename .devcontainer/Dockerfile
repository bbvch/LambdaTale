FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:5496e145288b93cd1c2bd52fcb12fdbb655206ddb3295fc9b1e460b0698d2e49
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
