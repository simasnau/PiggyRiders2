import React, { useState, createContext, useEffect} from 'react'; 
import {URL} from "../../Secrets"; 


export const SavingContext = createContext();

export const SavingProvider = props => {

    useEffect(() => {
        fetchItems();
    }, []);

    const [items, setItems] = useState([]);

    const fetchItems = async () => {
        const data = await fetch(URL+'/api/SavingsManagerInformations');

        const items = await data.json();
        console.log(items);
        setItems(items.data);
    };

    return (
        <SavingContext.Provider value={[items, setItems]}>
            {props.children}
        </SavingContext.Provider>
    );
};