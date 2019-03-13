<?php
namespace App\Model\Table;

use Cake\ORM\Query;
use Cake\ORM\RulesChecker;
use Cake\ORM\Table;
use Cake\Validation\Validator;

/**
 * Intcharacters Model
 *
 * @method \App\Model\Entity\Intcharacter get($primaryKey, $options = [])
 * @method \App\Model\Entity\Intcharacter newEntity($data = null, array $options = [])
 * @method \App\Model\Entity\Intcharacter[] newEntities(array $data, array $options = [])
 * @method \App\Model\Entity\Intcharacter|bool save(\Cake\Datasource\EntityInterface $entity, $options = [])
 * @method \App\Model\Entity\Intcharacter|bool saveOrFail(\Cake\Datasource\EntityInterface $entity, $options = [])
 * @method \App\Model\Entity\Intcharacter patchEntity(\Cake\Datasource\EntityInterface $entity, array $data, array $options = [])
 * @method \App\Model\Entity\Intcharacter[] patchEntities($entities, array $data, array $options = [])
 * @method \App\Model\Entity\Intcharacter findOrCreate($search, callable $callback = null, $options = [])
 */
class IntcharactersTable extends Table
{

    /**
     * Initialize method
     *
     * @param array $config The configuration for the Table.
     * @return void
     */
    public function initialize(array $config)
    {
        parent::initialize($config);

        $this->setTable('intcharacters');
        $this->setDisplayField('Id');
        $this->setPrimaryKey('Id');
    }

    /**
     * Default validation rules.
     *
     * @param \Cake\Validation\Validator $validator Validator instance.
     * @return \Cake\Validation\Validator
     */
    public function validationDefault(Validator $validator)
    {
        $validator
            ->integer('Id')
            ->allowEmptyString('Id', 'create');

        $validator
            ->scalar('Name')
            ->maxLength('Name', 30)
            ->requirePresence('Name', 'create')
            ->allowEmptyString('Name', false);

        $validator
            ->integer('Speed')
            ->requirePresence('Speed', 'create')
            ->allowEmptyString('Speed', false);

        $validator
            ->integer('Jump')
            ->requirePresence('Jump', 'create')
            ->allowEmptyString('Jump', false);

        $validator
            ->integer('Hp')
            ->requirePresence('Hp', 'create')
            ->allowEmptyString('Hp', false);

        $validator
            ->integer('Power')
            ->requirePresence('Power', 'create')
            ->allowEmptyString('Power', false);

        $validator
            ->integer('Item')
            ->requirePresence('Item', 'create')
            ->allowEmptyString('Item', false);

        return $validator;
    }
}
