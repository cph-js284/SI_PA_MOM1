# SI_PA_MOM1
Programming Assignment MOM1


# How to make it go

1) Start RabbitMQ in a docker container (This container will need 10'ish secs to spin up the rabbit inside, before you execute the next command)
```
sudo docker run -d --name SomeAddRabbit -p 5672:5672 -p 15673:15672 rabbitmq:3-management
```
2) Start the Travel agency addspammer
```
sudo docker run -it --rm --link SomeAddRabbit cphjs284/si_traveladds
```
3) Start a client - in a new terminal, this client is only interested in winter travel
```
sudo docker run -it --rm --link SomeAddRabbit cphjs284/si_travelcustomer "winter.#"
```
4) Start a client - in a new terminal, this client is only interested in traveling inside europe
```
sudo docker run -it --rm --link SomeAddRabbit cphjs284/si_travelcustomer "*.europe.*"
```
5) Start a client - in a new terminal, this client is only interested in lowfi-budget travel
```
sudo docker run -it --rm --link SomeAddRabbit cphjs284/si_travelcustomer "#.lowfi"
```


# Clean up 
Remove rabbitMQ container
```
sudo docker rm -f SomeAddRabbit
```
