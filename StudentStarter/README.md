# Secure Gateway Workshop

## Requirements
.NET 8 SDK locally, Docker Engine and Docker Compose plugin on Ubuntu VPS, SSH access.

## Architecture
Client -> gateway (port 5000) -> private function service (port 8080 inside Docker network).
The pre-built function is an ASP.NET Core HTTP endpoint simulating an Azure Function locally; it is **not** an Azure Functions runtime. This avoids adding Azure tooling to the two-hour workshop.

## Local testing (without Docker)
Run `dotnet run --project FunctionService --urls http://localhost:5055` in one terminal.
In a second terminal, set `FunctionUrl=http://localhost:5055`, `Jwt__Key` (32+ random characters), and `DEMO_PASSWORD` in your shell, then run `dotnet run --project Gateway --urls http://localhost:5000`.
Open http://localhost:5000/swagger. Login with admin and your configured password.

## VPS deployment
Install Docker Engine and Compose plugin first. Copy project to VPS (git clone or scp). In project root:
```
cp .env.example .env
# Edit .env: set JWT_KEY and DEMO_PASSWORD; do not commit .env
chmod 600 .env
docker compose up -d --build
docker compose ps
docker compose logs --tail=50 gateway
```
Allow TCP port 5000 in the VPS firewall and cloud NSG/security group for the class demo (prefer source-IP restriction). Open `http://VPS_PUBLIC_IP:5000/swagger`.

## API test (replace token)
```
curl -i http://VPS_PUBLIC_IP:5000/api/tasks -H 'Content-Type: application/json' -d '{"task":"Check stock"}'
curl -s http://VPS_PUBLIC_IP:5000/api/auth/login -H 'Content-Type: application/json' -d '{"username":"admin","password":"YOUR_DEMO_PASSWORD"}'
curl -i http://VPS_PUBLIC_IP:5000/api/tasks -H 'Authorization: Bearer YOUR_TOKEN' -H 'Content-Type: application/json' -d '{"task":"Check stock"}'
```
Expect 401 before login, 200 after login, 401 for malformed token. A valid token without Admin role should produce 403.

## Important
HTTP, Swagger exposure, shared demo credentials, and plain-text comparison are for a short isolated classroom demonstration ONLY. For production, use HTTPS, a real identity provider or hashed password store, secret management, rate limiting, restricted network access, and disable public Swagger.
