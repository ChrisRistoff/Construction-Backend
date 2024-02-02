#!/bin/bash
# Determine the host IP here. This example uses the default gateway as the host IP in a Linux environment.
HOST_IP=$(ip route | awk 'NR==1 {print $3}')

# Set the connection string environment variable with the dynamically determined host IP
export ConnectionStrings__DockerTestConnection="Host=$HOST_IP;Port=5432;Database=construction_test;Username=krasyo;Password=password;"

# Start your .NET application
dotnet test
