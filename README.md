# Acme.Automation

[![Build Status](https://dev.azure.com/sbaudart/GithubWorker/_apis/build/status/simonbaudart.Acme.Automation?branchName=master)](https://dev.azure.com/sbaudart/GithubWorker/_build/latest?definitionId=18&branchName=master)

Goal of this project is to create a Geek version of an IFTT'like.
All the configuration of the project is stored into a json or anything else and the core provide an engine to run all the rules.
The rules will be updated with Pull Request.

## Disclaimer

This is a hobby project and before any technical information, I have to start with a small disclaimer.

**USE IT AT YOUR OWN RISK**

I only give you here some tips and tricks that I use to replace IFTTT because I think this is useful.
I don't give any support, I'm not responsible of any data loss that may occur.


And now, let's start with the fun !

## Main process

Everything starts with a job. The job can be "RunAtStartup" or "CronScheduled".
A job is associated with a connector. 

1 - The worker will call the connector to retrieve messages from its source.

2 - The worker will take the actions from the configuration and execute each action on each message if the rule matches. In this first part, all actions are ran on a message before next message.

3 - The worker will take the groupedActions from the configuration and execute each action on all messages that matches the rule. In this first part, each action are ran on all message before next action.

# Main configuration

This is the base of the configuration.

```json
{
  "jobs": [
    {
      "id": "technical-unique-id-for-the-job",
      "friendlyName": "A friendly name for the job",
      "runAtStartup": true,
      "cronSchedule": "* * * * *",
      "connector": "technical-unique-id-for-the-connector",
      "activator": "technical-unique-id-for-the-activator",
      "actions": [
        {
          "type" : "simple",
          "rule": "technical-unique-id-for-the-rule",
          "processor": "technical-unique-id-for-the-processor"
        },
        {
          "type" : "sequence",
          "actions" : 
          [
            {
              "type" : "simple",
              "rule": "technical-unique-id-for-the-rule-1",
              "processor": "technical-unique-id-for-the-processor"
            },
            {
              "type" : "simple",
              "rule": "technical-unique-id-for-the-rule-2",
              "processor": "technical-unique-id-for-the-processor"
            }
          ]
        }
      ]
    }
  ],
  "connectors": [
    ...
  ],
  "rules": [
    ...
  ],
  "processors": [
    ...
  ]
}
```

# Available parts

## Actions

## simple
Run an processor if the rule matches.

## sequence
Run multiples actions on the message, each message is retrieved after the action and sent to the next actions.

## Activators

### PingActivator
Just for fun, launch a ping message in the queue.

### SmtpServerActivator
SMTP server that will listen on ports and send a message in the queue if a mail arrived.

#### Configuration
```json
{
  "id": "smtp-server-acme-automation.tech",
  "friendlyName": "Start a new Smtp Server",
  "type": "Acme.Automation.Activators.SmtpServerActivator, Acme.Automation.Activators",
  "config": {
    "serverName": "localhost",
    "ports": [
      25,
      587
    ]
  }
}
```

### Output
The message will contains the following data :
* sender : sender of the mail.
* recipient : recipient of the mail. If there is multiple recipients, a message will be created for each.
* date : date of the email.
* subject : subject of the email.
* htmlBody : the html body of the email.
* textBody : the text body of the email.
* attachments : a list of FileData for each attachment.

## Connectors

### Pop3 connector
This connector will retrieve message from a pop3 source.

#### Configuration

```json
{
  "id": "id-of-the-connector",
  "friendlyName": "Get emails from any@email.com",
  "type": "Acme.Automation.Connectors.Pop3Connector, Acme.Automation.Connectors",
  "config": {
    "host": "pop.email.com",
    "port": 995,
    "useSsl": true,
    "username": "any@email.com",
    "password": "mybigpassword"
  }
}
```

### Output
The message will contains the following data :
* sender : sender of the mail.
* recipient : recipient of the mail. If there is multiple recipients, a message will be created for each.
* date : date of the email.
* subject : subject of the email.
* htmlBody : the html body of the email.
* textBody : the text body of the email.
* attachments : a list of FileData for each attachment.

## Rules

### Always Match
This rule always return true :)

#### Configuration

```json
{
  "id": "always-match",
  "friendlyName": "Always return true",
  "type": "Acme.Automation.Rules.AlwaysMatch, Acme.Automation.Rules"
}
```

### Curve Receipt
Return true if the message property "subject" contains "Curve Receipt:"

#### Configuration

```json
{
  "id": "curve-receipt",
  "friendlyName": "Curve Receipt",
  "type": "Acme.Automation.Rules.CurveReceipt, Acme.Automation.Rules"
}
```

### Has Properties
Return true if the message contains all the specified properties.

#### Configuration

```json
{
  "id": "has-property",
  "friendlyName": "Check if message has properties 1, 2 and 3",
  "type": "Acme.Automation.Rules.HasProperties, Acme.Automation.Rules",
  "config": [
    "property-1",
    "property-2",
    "property-3"
  ]
}
```

### PropertyMatchRegex
Return true if the property match the regex.

#### Configuration

```json
{
  "id": "is-to-blabla.com",
  "friendlyName": "Is it an email to blabla.com ?",
  "type": "Acme.Automation.Rules.PropertyMatchRegex, Acme.Automation.Rules",
  "config": {
    "property": "recipient",
    "match" : "@blabla\\.com$"
  }
}
```

## Processors

### Dump Data
Dump all the message properties to the console.

#### Configuration

```json
{
  "id": "dump-data",
  "friendlyName": "Dump data to the console",
  "type": "Acme.Automation.Processors.DumpData, Acme.Automation.Processors"
}
```

### Curve Receipt
Read the textBody property from a message and parse it into an new property "transaction" in the message.

#### Configuration

```json
{
  "id": "curve-receipt",
  "friendlyName": "Curve Receipt",
  "type": "Acme.Automation.Processors.CurveReceipt, Acme.Automation.Processors"
}
```

### Wallet CSV
Read the transaction property in the message and create a csv. Send the csv as attachment in an email.

#### Configuration

```json
{
  "id": "wallet-csv",
  "friendlyName": "Wallet Csv Import",
  "type": "Acme.Automation.Processors.WalletCsvImport, Acme.Automation.Processors",
  "config": {
    "sender" : "from@mail.com",
    "recipient": "to@mail.com",
    "smtp": {
      "host": "smtp@mail.com",
      "port": 587,
      "username": "username",
      "password": "xxxxxxxx"
    }
  }
}
```

### Forward Email
Read the information from the message (same as the connectors) and forward the mail to the recipient in the configuration.

#### Configuration

```json
{
  "id": "forward-mail",
  "friendlyName": "Forward email to to@mail.com",
  "type": "Acme.Automation.Processors.ForwardEmail, Acme.Automation.Processors",
  "config": {
    "sender" : "from@mail.com",
    "recipient": "to@mail.com",
    "smtp": {
      "host": "smtp@mail.com",
      "port": 587,
      "username": "username",
      "password": "xxxxxxxx"
    }
  }
}
```

# Sample Usages

See in the Samples directory.

* [curve-receipt-to-wallet.json](https://github.com/simonbaudart/Acme.Automation/blob/master/Samples/curve-receipt-to-wallet.json) : Import the Curve Email receipt into Wallet.

# Buy Me A Beer
If this project help you, you can give me a beer :) 

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=FER3YNBKV9DEA)