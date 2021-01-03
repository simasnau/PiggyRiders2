export default (state, action) => {
    switch (action.type) {
        case 'GET_TRANSACTION':
            return {
                ...state,
                transactions: action.payload
            }
        case 'ADD_TRANSACTION':
            return {
                ...state,
                transactions: [...state.transactions, action.payload]
            }
        case 'GET_BALANCE':
            return {
                ...state,
                balance: action.payload
            }
        default:
            return state;
    }
}