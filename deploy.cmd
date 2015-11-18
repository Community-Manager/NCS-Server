    @echo off
    IF "%SITE_FLAVOR%" == "Server" (
      deploy.Server.cmd
    ) ELSE (
      IF "%SITE_FLAVOR%" == "UI" (
        deploy.UI.cmd
      ) ELSE (
        echo You have to set SITE_FLAVOR setting to either "Server" or "UI"
        exit /b 1
      )
    )