# Summary
In service layer there are 3 repositories to deal with the data:\
**CardRepository** - Create card (card format is 15 digits).\
**PaymentRepository** - To perform Payments and the the balance.\
**SecurityRepository** - Login to user and validate requests.\

**.NET Framework**: .NET 8\
**Language**: C#\
**ORM**: Entity Framework (InMemory, ready to run)\

## Data stored in memory
- **Package used**: Microsoft.EntityFrameworkCore.InMemory

**OnModelCreating** is seeding the initial data every time the application runs.

### Initial SubLeder
![image](https://github.com/user-attachments/assets/dc602cdd-5a70-47ff-990d-d67c552f2b7f)


## Card Management
By Default the CreditCards are created with a credit limit of 10k

 ## Payment Fees
 **SubLedger** contains the details of the payments
 **Ledger**
 **Universal Fees Exchange (UFE)** is created Randomly each 1
 - **Package used**: Microsoft.Extensions.Caching.Memory;

# API
I added Swagger to can test alternativally to Postman
## Routes
**Financial**
- [POST] /PaymentPost
- [GET] /Balance

**Home**
- [POST] /Login

# Security
I used Cache to store the token in the calls, I utilized an IAuthorizationFilter to perform the validations, and Applied the authorization filter for actions methods that need authentication.

# Testing
Test Framework used: **MSTest**
I did not included Moq framework due to it is using a InMomory DB.

## API (Swagger)
### Login
![image](https://github.com/user-attachments/assets/14402e4d-13bf-44b8-b56f-9037b66b03a3)

### Balance
![image](https://github.com/user-attachments/assets/14d2f30f-44b7-4e89-b78e-9eac1ae6306d)

### Payment
![image](https://github.com/user-attachments/assets/2a3276de-4e47-4523-965a-d9dcd633ddef)


## UI
### Login
![image](https://github.com/user-attachments/assets/0ece7e84-7477-4f82-be9d-22d9f4a1632a)

### Balance
![image](https://github.com/user-attachments/assets/e1881e13-edf1-42a2-841e-1ba04e7c5cf6)

### Payment
![image](https://github.com/user-attachments/assets/5d824c80-3619-4db4-8a5d-4806bab2b658)
