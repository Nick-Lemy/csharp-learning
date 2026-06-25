# ECommerce App with Event-Driven Architecture

A small demo showing event-driven architecture in C# using Kafka.

## Services

- **OrderService**: prompts the user for an email and a product, then publishes an `OrderPlaced` event to the `orders` topic.
- **InventoryService**: consumes `orders`, tries to reserve stock, then publishes an `OrderProcessed` event to `inventory-events`.
- **NotificationService**: consumes `inventory-events` and sends a real email (confirmation if stock was reserved, out of stock notice otherwise).

## How it flows

1. OrderService publishes `OrderPlaced`.
2. InventoryService reserves stock and publishes `OrderProcessed` with the result.
3. NotificationService reads the result and emails the customer.

Each service is independent. They only share events, never call each other directly.

## Requirements

- .NET 8 SDK
- Docker
- A Gmail account with an App Password (for NotificationService)

## Setup

### 1. Start Kafka

```bash
docker compose up -d
```

This starts a single Kafka broker in KRaft mode. The `orders` and `inventory-events` topics are created automatically the first time a service publishes to them.

If a topic does not get created automatically, create it manually:

```bash
docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --create --topic orders
docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --create --topic inventory-events
```

List the topics to confirm they exist:

```bash
docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --list
```

### 2. Configure email for NotificationService

NotificationService reads your Gmail credentials from environment variables. Export them in the same terminal you run it from:

```bash
export GMAIL_USER="your-email@gmail.com"
export GMAIL_APP_PASSWORD="your-app-password"
```

## Running

Open three terminals and run one service in each:

```bash
dotnet run --project NotificationService
dotnet run --project InventoryService
dotnet run --project OrderService
```

In the OrderService terminal, enter an email, pick a product, and enter a quantity. Watch InventoryService reserve the stock and NotificationService send the email.

## Trying out of stock

Each product starts with a fixed stock count in InventoryService. Order a low stock item like Headphones more times than available to see the out of stock path: InventoryService refuses the reservation and NotificationService sends an out of stock email instead of a confirmation.

## Stopping

```bash
docker compose down
```
