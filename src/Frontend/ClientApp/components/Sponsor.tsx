import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { QueryRenderer, graphql } from 'react-relay';

import { Environment, Network, RecordSource, Store } from 'relay-runtime'

  function fetchQuery(
    operation: any,
    variables: any,
    cacheConfig: any,
    uploadables: any,
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
    network,
    store,
  });
  
  //export default environment;

export class Sponsor extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return(
            <QueryRenderer 
                environment = { environment } 
                query={graphql`query ExampleQuery() {
                    page(url: "/about") {
                        url
                        content
                      }
                }`}
                variables={{}}
                render={({error, props}) => {
                    if(error){
                        return <div>{error.message}</div>;
                    } else if(props) {
                        return <div>{props.content}</div>;
                    }
                    return <div>Loading...</div>;
                }}
                />
        )
    }
}
