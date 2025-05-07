import React from 'react';
import { useQuery } from '@apollo/client';
import { LIBRARY_RESOURCES_QUERY } from '../graphql/queries';

function CultureLibrary() {
  const { loading, error, data } = useQuery(LIBRARY_RESOURCES_QUERY);

  if (loading) return <div>Loading...</div>;  
  if (error) return <div>Error: {error.message}</div>;

  return (
    <section>
      <h2>Culture Library</h2>
      <ul>
        {data.resources.map(resource => (  
          <li key={resource.id}>
            <h3>{resource.title}</h3>
            <p>{resource.description}</p>
            <a href={resource.url} target="_blank" rel="noopener noreferrer">View Resource</a>
          </li>
        ))}
      </ul>
    </section>
  );
}

export default CultureLibrary;