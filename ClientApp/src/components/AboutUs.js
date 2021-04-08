import React, { Component } from "react";


const divStyle={
    textAlign:"center",
}

const h1Style={
    margin:40   
}

const pStyle={
    fontSize:24
}


class AboutUs extends Component {
  static displayName = AboutUs.name;

  render() {
    return (
       <div style={divStyle}>
        <h1 style={h1Style}>Welcome to Smart saver!</h1>
        <p style={pStyle}>Our vision is a company which helps people to manage their finances, save money and reach their financial goals. In turn people can spend more time doing other activities rather than worrying about calculating budgets and saving money.</p>
        <p style={pStyle}>PiggyRiders idea is simple. Any person can create their account, login and start using it. Users can manage their income, expenses, see transaction history, set saving goals and spending limits.</p>
      </div>
    );
  }
}
export default AboutUs