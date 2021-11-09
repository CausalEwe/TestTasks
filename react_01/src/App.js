import logo from './logo.svg';
import './App.css';
import Login from './components/login/login';
import store from './redux/redux';

const App = (props) => {
  return (
    <div className="app-wrapper">
      <Login data={props.store.getState().formReducer} />;
    </div>
  );
};

export default App;
