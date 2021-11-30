import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';

export class EmployeeEdit extends Component {
  static displayName = EmployeeEdit.name;

  constructor(props) {
    super(props);
    this.state = { id: 0,fullName: '',birthdate: '',tin: '',typeId: 1, loading: true,loadingSave:false };
  }

  componentDidMount() {
    this.getEmployee(this.props.match.params.id);
  }
  handleChange(event) {
    this.setState({ [event.target.name] : event.target.value});
  }

  handleSubmit(e){
      e.preventDefault();
      if (window.confirm("Are you sure you want to save?")) {
        this.saveEmployee();
      } 
  }

  render() {

    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : <div>
    <form>
<div className='form-row'>
<div className='form-group col-md-6'>
  <label htmlFor='inputFullName4'>Full Name: *</label>
  <input type='text' className='form-control' id='inputFullName4' onChange={this.handleChange.bind(this)} name="fullName" value={this.state.fullName} placeholder='Full Name' />
</div>
<div className='form-group col-md-6'>
  <label htmlFor='inputBirthdate4'>Birthdate: *</label>
  <input type='date' className='form-control' id='inputBirthdate4' onChange={this.handleChange.bind(this)} name="birthdate" value={this.state.birthdate} placeholder='Birthdate' />
</div>
</div>
<div className="form-row">
<div className='form-group col-md-6'>
  <label htmlFor='inputTin4'>TIN: *</label>
  <input type='text' className='form-control' id='inputTin4' onChange={this.handleChange.bind(this)} value={this.state.tin} name="tin" placeholder='TIN' />
</div>
<div className='form-group col-md-6'>
  <label htmlFor='inputEmployeeType4'>Employee Type: *</label>
  <select id='inputEmployeeType4' onChange={this.handleChange.bind(this)} value={this.state.typeId}  name="typeId" className='form-control'>
    <option value='1'>Regular</option>
    <option value='2'>Contractual</option>
  </select>
</div>
</div>
<button type="submit" onClick={this.handleSubmit.bind(this)} disabled={this.state.loadingSave} className="btn btn-primary mr-2">{this.state.loadingSave?"Loading...": "Save"}</button>
<button type="button" onClick={() => this.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
</form>
</div>;


    return (
        <div>
        <h1 id="tabelLabel" >Employee Edit</h1>
        <p>All fields are required</p>
        {contents}
      </div>
    );
  }

  async saveEmployee() {
    if(this.state.birthdate == "")
    {
      alert("Invalid Date.");
      return;
    }

    if(this.state.fullName == "" || this.state.tin == "")
    {
      alert("Please fill all required fields.");
      return;
    }

    if(isNaN(this.state.tin))
    {
      alert("Input only numbers for TIN.");
      return;
    }


    this.setState({ loadingSave: true });

    try {
          const token = await authService.getAccessToken();
          const requestOptions = {
              method: 'PUT',
              headers: !token ? {} : { 'Authorization': `Bearer ${token}`,'Content-Type': 'application/json' },
              body: JSON.stringify(this.state)
          };
          const response = await fetch('api/employees/' + this.state.id,requestOptions);
          const data = await response.json();
      
          if(data.isSuccessful){
              this.setState({ loadingSave: false });
              alert("Employee successfully updated");
              this.props.history.push("/employees/index");
          }
          else{
              this.setState({ loadingSave: false });
              alert(data.error);
          }
    } catch (error) {
          this.setState({ loadingSave: false });
          alert("An error has occured. Please contact support.")
    }
  }

  async formatDate(data) {
    var dt = new Date(data).toLocaleDateString('en-CA');

    return dt;
  }

  async getEmployee(id) {

    this.setState({ loading: true,loadingSave: false });

    try {
          const token = await authService.getAccessToken();
          const response = await fetch('api/employees/' + id, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
          });
          const data = await response.json();
      
          if(data.isSuccessful){
            let employeeData = data.data;
            var bdate = await this.formatDate(employeeData.birthdate);
      
            this.setState({ id: employeeData.id, fullName: employeeData.fullName, birthdate: bdate, tin: employeeData.tin, typeId: employeeData.employeeTypeId, loading: false,loadingSave: false });
          }
          else{
              alert(data.error);
          }
    } catch (error) {
          this.setState({ loading: false, loadingSave: false });
          alert("An error has occured. Please contact support.")
    }

  }
}
