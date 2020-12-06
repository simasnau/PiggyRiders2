import React, {useContext} from 'react';
import { GlobalContext } from '../BMcontextAPI/GlobalState';

export const Transaction = ({ transaction }) => {
  const sign = transaction.amount < 0 ? '-' : '+';

  return (
    <li className={transaction.amount < 0 ? 'minus' : 'plus'}>
      {transaction.text} <span>{sign}${Math.abs(transaction.amount)}</span>
    </li>
  )
}