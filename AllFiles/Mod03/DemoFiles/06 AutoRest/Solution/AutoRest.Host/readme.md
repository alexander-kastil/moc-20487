Install AutoREST:

```
 npm install -g autorest@latest
```
Execute CMD:

```
autorest -Input http://localhost:5000/swagger/v1/swagger.json -CodeGenerator CSharp -Namespace AutoRest.Sdk
```