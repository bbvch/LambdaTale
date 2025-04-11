FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:ebdda0ef27534653a88e28c32b10446e78c86bcd1bae7f355a8ef995d3c312e9
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
