import React, { Component } from "react";
import {URL} from "../../Secrets";

let styles={
    h3Style:{
        margin:20
    },
    btnStyle:{
        width:"50%",
    },
    divStyle:{
        textAlign:"center"
    }

}

export default class DeleteUser extends Component {

    deleteUser =()=> {
        alert("User will be deleted");
        fetch(URL+"/api/UserInformations", {method: "DELETE"}).then(response=>{
            response.json().then(data=>{
                if(data.success){
                    alert("Request returned ok");
                    this.props.history.push("/log-in");
                }else {
                    alert("Request failed");
                }
            })
        })
    }

    render() {
        return (
        <div style={styles.divStyle}>
            <h3 style={styles.h3Style}>You are about to delete your account. Are you sure you want to do this?</h3>
            <button style={styles.btnStyle} type="submit" className="btn btn-primary" onClick={this.deleteUser}>
            Yes
            </button>
        </div>
        );
    }
}
