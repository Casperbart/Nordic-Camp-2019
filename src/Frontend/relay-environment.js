const {
    Environment,
    Network,
    RecordSource,
    Store,
  } = require('relay-runtime');

  function fetchQuery(
    operation,
    variables,
    cacheConfig,
    uploadables,
  ) {
    return fetch('http://nordic4hcampbackend.azurewebsites.net/v1/graphql', {
      method: 'POST',
      headers: {
        // Add authentication and other headers here
        'content-type': 'application/json'
      },
      body: JSON.stringify({
        query: operation.text, // GraphQL text from input
        variables,
      }),
    }).then(response => {
      return response.json();
    });
  }
  
  
  const store = new Store(new RecordSource())
  const network = Network.create(fetchQuery);
  const handlerProvider = null;
  
  const environment = new Environment({
    handlerProvider, // Can omit.
    network,
    store,
  });
  
  export default environment;