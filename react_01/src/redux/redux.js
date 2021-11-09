import { combineReducers, createStore } from 'redux';
import formReducer from './form-reducer';

let reducers = combineReducers({
  formReducer: formReducer,
});

let store = createStore(reducers);
export default store;
