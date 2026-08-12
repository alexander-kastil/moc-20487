# Prerequisites

Download & Install [VSCode](https://code.visualstudio.com/download)

## Azure CLI

[Install Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)

[Azure CLI Reference](https://docs.microsoft.com/en-us/cli/azure/reference-index?view=azure-cli-latest)

## Certificates

Enable Chrome to trust self-signed localhost certs:

```
chrome://flags/#allow-insecure-localhost
```

Certificate Troubleshooting

```
dotnet dev-certs https --clean
dotnet dev-certs https -t
```

## Running .NET Core

Download NuGet Packages

`dotnet restore`

Run Application

`dotnet run` | `dotnet watch run`

Run .Net Core that is available using IP-Address

```
dotnet run --urls http://0.0.0.0:5000 or dotnet run --urls https://0.0.0.0:5001
```

## License & Re-Use

Copyright (c) 2026 Alexander Kastil / integrations.at. All rights reserved. These materials are proprietary and are **not** open-source or Creative Commons. Use is governed by the [LICENSE](LICENSE) file; the summary below is for convenience only and does not modify it.

- Personal Use only: a natural person may use the materials for their own private self-study, free of charge.
- Any use by or for a business, public body, school, university, association, or non-profit organization is prohibited without a paid written license. This includes learning partners, training providers, consultancies, and employers.
- Delivering the materials as a training, workshop, or course by any trainer or learning partner is prohibited without a paid written license, **whether the delivery is paid or free of charge, and whether the deliverer is for-profit or non-profit**.
- Redistribution, publication, hosting, and distribution of derivative works are not permitted.
- Governed by Austrian law; exclusive place of jurisdiction is Vienna, Austria (see the LICENSE for the full terms).

Participation in this project is also governed by the [Code of Conduct](CODE_OF_CONDUCT.md).

For a commercial, non-profit, or training license, contact the author via [LinkedIn](https://www.linkedin.com/in/alexander-kastil/) or [email](mailto:alexander.kastil@integrations.at).
