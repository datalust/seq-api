# Seq Enable AAD (Azure Active Directory)

Be sure to read the [Seq Azure Active Directory documentation](https://docs.datalust.co/docs/azure-active-directory) to find the manual AAD setup instructions.

## Example usage:

```
seq-enable-aad.exe https://seq.example.com --uname=example@microsoft.com --clientid=xxxxxx --tenantid=xxxxxx --clientkey=xxxxxx --authority=login.windows.net
```

### **Important note:**

#### Windows

Don't forget to set the "canonical URI" which Seq uses as a reply address for AAD.

```
seq config -k api.canonicalUri -v https://seq.example.com
seq service restart
```

#### Linux / Docker

Don't forget to include the BASE_URI which Seq uses as a reply address for AAD.

```
docker run -d \
  --restart unless-stopped \
  --name seq \
  -p 5341:80 \
  -e ACCEPT_EULA=Y \
  -e BASE_URI=https://seq.example.com \
  datalust/seq:latest
```