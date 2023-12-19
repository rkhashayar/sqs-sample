variable "queue_name"{
    description = "sql sample queue name"
    type= string
}

variable "retention_period"{
    description = "How long message must live in queue"
    type=number
    default = 8000
}

variable "visibility_timeout" {
    description = "How long message is visible to consumers before removed"
    type = number
    default = 60
}

variable "recieve_count" {
    description = "Number of times a message can be received until it goes to DQL"
    type = number
    default = 3
}