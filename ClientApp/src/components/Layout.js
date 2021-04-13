import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu basename={this.props.basename}/>
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
