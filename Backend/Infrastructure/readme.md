## Run emulator
Run this command: 
```
docker compose -f docker-compose.yaml up -d
```

## Connect to emulator
Use this connection string on DEV: 
```
Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;
```
Use service bus FQDN when running in Azure:
```
<NAMESPACE-NAME>.servicebus.windows.net
```