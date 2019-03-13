<?php
namespace App\Controller;

use App\Controller\AppController;

/**
 * Intcharacters Controller
 *
 * @property \App\Model\Table\IntcharactersTable $Intcharacters
 *
 * @method \App\Model\Entity\Intcharacter[]|\Cake\Datasource\ResultSetInterface paginate($object = null, array $settings = [])
 */
class IntcharactersController extends AppController
{

    /**
     * Index method
     *
     * @return \Cake\Http\Response|void
     */
    public function index()
    {
        $intcharacters = $this->paginate($this->Intcharacters);

        $this->set(compact('intcharacters'));
    }

    /**
     * View method
     *
     * @param string|null $id Intcharacter id.
     * @return \Cake\Http\Response|void
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function view($id = null)
    {
        $intcharacter = $this->Intcharacters->get($id, [
            'contain' => []
        ]);

        $this->set('intcharacter', $intcharacter);
    }

    /**
     * Add method
     *
     * @return \Cake\Http\Response|null Redirects on successful add, renders view otherwise.
     */
    public function add()
    {
        $intcharacter = $this->Intcharacters->newEntity();
        if ($this->request->is('post')) {
            $intcharacter = $this->Intcharacters->patchEntity($intcharacter, $this->request->getData());
            if ($this->Intcharacters->save($intcharacter)) {
                $this->Flash->success(__('The intcharacter has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The intcharacter could not be saved. Please, try again.'));
        }
        $this->set(compact('intcharacter'));
    }

    /**
     * Edit method
     *
     * @param string|null $id Intcharacter id.
     * @return \Cake\Http\Response|null Redirects on successful edit, renders view otherwise.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function edit($id = null)
    {
        $intcharacter = $this->Intcharacters->get($id, [
            'contain' => []
        ]);
        if ($this->request->is(['patch', 'post', 'put'])) {
            $intcharacter = $this->Intcharacters->patchEntity($intcharacter, $this->request->getData());
            if ($this->Intcharacters->save($intcharacter)) {
                $this->Flash->success(__('The intcharacter has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The intcharacter could not be saved. Please, try again.'));
        }
        $this->set(compact('intcharacter'));
    }

    /**
     * Delete method
     *
     * @param string|null $id Intcharacter id.
     * @return \Cake\Http\Response|null Redirects to index.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function delete($id = null)
    {
        $this->request->allowMethod(['post', 'delete']);
        $intcharacter = $this->Intcharacters->get($id);
        if ($this->Intcharacters->delete($intcharacter)) {
            $this->Flash->success(__('The intcharacter has been deleted.'));
        } else {
            $this->Flash->error(__('The intcharacter could not be deleted. Please, try again.'));
        }

        return $this->redirect(['action' => 'index']);
    }
}
