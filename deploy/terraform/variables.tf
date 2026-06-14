variable "environment" {
  description = "The environment (e.g., dev, prod)"
  type        = string
  default     = "dev"
}

variable "location" {
  description = "The Azure region to deploy resources"
  type        = string
  default     = "westeurope"
}

variable "project_name" {
  description = "The base name for the project resources"
  type        = string
  default     = "renting"
}
