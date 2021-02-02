import React, { Component } from 'react';
import { SearchBar } from './SearchBar';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <SearchBar />
      </div>
    );
  }
}
